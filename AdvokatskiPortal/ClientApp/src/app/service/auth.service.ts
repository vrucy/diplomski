import { SuccessfullLoginComponent } from './../snackBar/successfull-login/successfull-login.component';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { SuccessfullRegistrationComponent } from '../snackBar/successfull-registration/successfull-registration.component';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  typeUserValue: string;
  isLogin: Boolean = false;
  type: string;
  currentUserName;

  constructor(private http: HttpClient, private router: Router, private _snackBar: MatSnackBar) { }

  Registration(user) {
    return this.http.post<any>('http://localhost:44345/api/Account/registration', user).subscribe(res => {
      //localStorage.setItem('token', res);
      //this.autorization(res);
      this._snackBar.openFromComponent(SuccessfullRegistrationComponent, {
        duration: 2000
      });
      this.router.navigate(['/Login']);
    });
  }
  Login(user) {
    this.http.post<any>('http://localhost:44345/api/Account/Login', user).subscribe(res => {
      this._snackBar.openFromComponent(SuccessfullLoginComponent, {
        duration: 2000
      });
      this.currentUserName = res.user;
      this.http.get(`http://localhost:44345/api/Account/getCurrentUser/${this.currentUserName}`).subscribe(res => {
        localStorage.setItem('CurrentUser', JSON.stringify(res));

      })
      localStorage.setItem('Token', res);
      //this.router.navigate(['/pocetnaKorisnik']);
      this.autorization(res);
    });

  }
  //where I used this in app
  getUser(){
    return this.http.get('http://localhost:44345/api/Account/getUser')
  }
  getCraftman(){
    return this.http.get('http://localhost:44345/api/Account/getCraftman')
  }
  isLogged(): boolean {
    const user = localStorage.getItem('Token');
    if (user != null) {
      return true;
    } 
    else {
      return false;
    }
  }
  registrationCraftman(craftman) {
    return this.http.post<any>('http://localhost:44345/api/Account/RegistrationCraftman', craftman).subscribe(res => {
      this._snackBar.openFromComponent(SuccessfullRegistrationComponent, {
        duration: 2000
      });
      this.router.navigate(['/Login']);
    })
  }
  editProfilUser(user) {
    return this.http.put<any>('http://localhost:44345/api/Account/EditUser', user).subscribe();
  }
  editProfilCraftman(craftman) {
    return this.http.put('http://localhost:44345/api/Account/EditCraftman', craftman).subscribe();
  }
  //TODO refator!!!
  autorization(res) {
    let tokenValue = res['token'];
    localStorage.setItem('UserName', res['user']);
    localStorage.setItem('token', tokenValue);
    this.typeUserValue = res["typeOfClaim"]
    localStorage.setItem('typeUser', this.typeUserValue);
    this.isLogin = true;
    this.type = localStorage.getItem('typeUser');

    switch (this.type) {
      case "AdminCraftman": {
        this.router.navigate(['/ReviewContract'])
        return true;
      }
      case "RegularCraftman": {
            this.router.navigate(['/ReviewContract']);
        return true;
      } case "RegularUser": {
            this.router.navigate(['/TableCraftmans']);
        return true;
      } default: {
        break;
      }
    }
  }

  logout() {
    this.router.navigate(['Login']);
    localStorage.removeItem('Token');
    localStorage.removeItem('typeUser');
  }
}
