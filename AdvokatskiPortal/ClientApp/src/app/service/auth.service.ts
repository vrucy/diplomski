import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  typeUserValue: string;
  isLogin: Boolean;
  type: string;

  constructor(private http: HttpClient, private router: Router) { }
  registration(korisnik) {

    return this.http.post<any>('http://localhost:44345/api/Account/registration', korisnik).subscribe(res => {
      localStorage.setItem('token', res);
      this.authenticate(res)
    });
}
  login(korisnik) {
    return this.http.post<any>('http://localhost:44345/api/Account/login', korisnik).subscribe(res => {
      localStorage.setItem('token', res);
      this.router.navigate(['/pocetnaKorisnik'])
      this.authenticate(res)
    });
}
registrationAdvokat(advokat){
  return this.http.post<any>('http://localhost:44345/api/Account/registrationAdvokat', advokat).subscribe(res => {
    localStorage.setItem('token', res);
    this.authenticate(res);
  })
}
authenticate(res) {
  let tokenValue = res['token'];
  console.log(tokenValue);
  localStorage.setItem('token', tokenValue);
  this.typeUserValue = res["typeOfClaim"]
  localStorage.setItem('typeUser', this.typeUserValue);
  this.isLogin = true;
  this.type = localStorage.getItem('typeUser');

  switch (this.type) {
      case "AdminAdvokat": {
          console.log("admin work")
          // home page admin
          this.router.navigate(['/pocetnaAdvokat'])
          return true;
      }
      case "RegularAdvokat": {
          this.router.navigate(['/pocetnaAdvokat']);
          return true;
      } case "RegularUser": {
          console.log("User")
          this.router.navigate(['/pocetnaKorisnik']);
          return true;
      } default: {
          break;
      }
  }
}

logout() {
  this.router.navigate(['login'])
  localStorage.removeItem('token')
  localStorage.removeItem('typeUser')
}
}
