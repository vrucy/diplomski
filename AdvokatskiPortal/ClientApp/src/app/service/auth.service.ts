import { UspesnoLogovanjeComponent } from './../snackBar/uspesno-logovanje/uspesno-logovanje.component';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  typeUserValue: string;
  isLogin: Boolean = false;
  type: string;
  trenutniKorisnikKorisnikoIme;

  constructor(private http: HttpClient, private router: Router, private _snackBar: MatSnackBar) { }

  registration(korisnik) {
    return this.http.post<any>('http://localhost:44345/api/Account/registration', korisnik).subscribe(res => {
      localStorage.setItem('token', res);
      this.authenticate(res);
    });
  }
  login(korisnik) {
    this.http.post<any>('http://localhost:44345/api/Account/login', korisnik).subscribe(res => {
      this._snackBar.openFromComponent(UspesnoLogovanjeComponent, {
        duration: 2000
      });
      this.trenutniKorisnikKorisnikoIme = res.user;
      this.http.get(`http://localhost:44345/api/Account/getCurrentUser/${this.trenutniKorisnikKorisnikoIme}`).subscribe(res => {
        localStorage.setItem('trenutniKorisnik', JSON.stringify(res));

      })
      localStorage.setItem('token', res);
      this.router.navigate(['/pocetnaKorisnik']);
      this.authenticate(res);
    });

  }
  getKorisnik(){
    return this.http.get('http://localhost:44345/api/Account/getKorisnik')
  }
  getMajstor(){
    return this.http.get('http://localhost:44345/api/Account/getMajstor')
  }
  isLogged(): boolean {
    const user = localStorage.getItem('token');
    if (user != null) {
      return true;
    } else {
      return false;
    }
  }
  registrationAdvokat(majstor) {
    return this.http.post<any>('http://localhost:44345/api/Account/registrationAdvokat', majstor).subscribe(res => {
      localStorage.setItem('token', res);
      this.authenticate(res);
    })
  }
  editProfilKorisnik(korisnik) {
    return this.http.put<any>('http://localhost:44345/api/Account/editKorisnik', korisnik).subscribe();
  }
  editProfilMajstor(majstor) {
    return this.http.put('http://localhost:44345/api/Account/editMajstor', majstor).subscribe();
  }
  authenticate(res) {
    let tokenValue = res['token'];
    localStorage.setItem('userName', res['user']);

    localStorage.setItem('token', tokenValue);
    this.typeUserValue = res["typeOfClaim"]
    localStorage.setItem('typeUser', this.typeUserValue);
    this.isLogin = true;
    this.type = localStorage.getItem('typeUser');

    switch (this.type) {
      case "AdminAdvokat": {
        this.router.navigate(['/pocetnaAdvokat'])
        return true;
      }
      case "RegularAdvokat": {
        this.router.navigate(['/pocetnaAdvokat']);
        return true;
      } case "RegularUser": {
        this.router.navigate(['/pocetnaKorisnik']);
        return true;
      } default: {
        break;
      }
    }
  }

  logout() {
    this.router.navigate(['login']);
    localStorage.removeItem('token');
    localStorage.removeItem('typeUser');
  }
}
