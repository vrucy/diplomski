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

    return this.http.post<any>('https://localhost:44345/api/Account/registration', korisnik).subscribe(res => {
      localStorage.setItem('token',res);
      this.authenticate(res)
    });
}
  login(korisnik) {
    return this.http.post<any>("https://localhost:44345/api/Account/login", korisnik).subscribe(res => {
      localStorage.setItem('token', res);
      this.router.navigate(['/pocetnaKorisnik'])
      this.authenticate(res)
    });
}
authenticate(res) {
  let tokenValue = res['token'];
  console.log(tokenValue);
  localStorage.setItem('token', tokenValue);
  //treba route navesti na home page. posebno na advokata posebno za korisnika
  this.typeUserValue = res["typeOfClaim"]
  localStorage.setItem('typeUser', this.typeUserValue);
  this.isLogin = true;
  this.type = localStorage.getItem('typeUser');

  switch (this.type) {
      case "AdminAdvokat": {
          console.log("admin work")
          // home page admin
          this.router.navigate(['/homePageAdvokat'])
          return true;
      }
      case "RegularAdvokat": {
          console.log("regular work")
          //home page admin
          this.router.navigate(['/tabelaSaAdvokatima'])
          return true;
      } case "RegularUser": {
          console.log("User")
          // home page regular user
          this.router.navigate(['/homePageKorisnik'])
          return true;
      } default: {
          // never happened
          console.log("Invalid choice");
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
