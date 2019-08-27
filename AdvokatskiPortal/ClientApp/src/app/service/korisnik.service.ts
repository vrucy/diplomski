import { Advokat } from './../model/Advokat';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Slucaj } from '../model/Slucaj';

@Injectable({
  providedIn: 'root'
})
export class KorisnikService {

  constructor(private http: HttpClient) { }
  getAllAdvokati(): Observable<Advokat[]>{
    return this.http.get<Advokat[]>('http://localhost:44345/api/Advokat');
  }
  getAllSlucajForKorisnik(): Observable<Slucaj[]>{
    return this.http.get<Slucaj[]>('http://localhost:44345/api/Korisnik/getAllSlucajForKorisnik');
  }
  kreiranjeSlucaja(slucaj){
    return this.http.post('http://localhost:44345/api/Korisnik/kreiranjeSlucaja', slucaj).subscribe(rez => {
      console.log(rez)
    });
  }
  postSlucajaSaAdvokatimaSaCenovnikom(slucaj){
    return this.http.post('http://localhost:44345/api/Korisnik/postSlucajaSaAdvokatimaSaCenovnikom', slucaj).subscribe(res => {
      console.log(res)
    }) 
  }
  postSlucajAdvokatima(slucaj){
    return this.http.post('http://localhost:44345/api/Korisnik/postSlucajAdvokatima', slucaj).subscribe(rez => {
    });
  }
  postRequestAdvokats(advokati){
    return this.http.post('http://localhost:44345/api/Korisnik/postRequestAdvokats',advokati).subscribe();
  }
  
}
