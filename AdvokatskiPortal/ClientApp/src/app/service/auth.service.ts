import { UspesnoLogovanjeComponent } from './../snackBar/uspesno-logovanje/uspesno-logovanje.component';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { UspesnaRegistracijaComponent } from '../snackBar/uspesna-registracija/uspesna-registracija.component';

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
      //localStorage.setItem('token', res);
      //this.autorization(res);
      this._snackBar.openFromComponent(UspesnaRegistracijaComponent, {
        duration: 2000
      });
      this.router.navigate(['/login']);
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
      //this.router.navigate(['/pocetnaKorisnik']);
      this.autorization(res);
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
    } 
    else {
      return false;
    }
  }
  registrationMajstor(majstor) {
    return this.http.post<any>('http://localhost:44345/api/Account/registrationMajstor', majstor).subscribe(res => {
      this._snackBar.openFromComponent(UspesnaRegistracijaComponent, {
        duration: 2000
      });
      this.router.navigate(['/login']);
    })
  }
  editProfilKorisnik(korisnik) {
    return this.http.put<any>('http://localhost:44345/api/Account/editKorisnik', korisnik).subscribe();
  }
  editProfilMajstor(majstor) {
    return this.http.put('http://localhost:44345/api/Account/editMajstor', majstor).subscribe();
  }
  autorization(res) {
    let tokenValue = res['token'];
    localStorage.setItem('userName', res['user']);
    localStorage.setItem('token', tokenValue);
    this.typeUserValue = res["typeOfClaim"]
    localStorage.setItem('typeUser', this.typeUserValue);
    this.isLogin = true;
    this.type = localStorage.getItem('typeUser');

    switch (this.type) {
      case "AdminMajstor": {
        this.router.navigate(['/pocetnaMajstor'])
        return true;
      }
      case "RegularMajstor": {
            this.router.navigate(['/pocetnaMajstor']);
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
