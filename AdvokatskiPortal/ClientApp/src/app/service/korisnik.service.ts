import { Majstor } from './../model/Majstor';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Slucaj } from '../model/Slucaj';

@Injectable({
  providedIn: 'root'
})
export class KorisnikService {

  constructor(private http: HttpClient) { }
  getAllAdvokati(){
    return this.http.get('http://localhost:44345/api/Korisnik/getAllMajstori');
  }
  getAllSlucajForKorisnik() {
    return this.http.get ('http://localhost:44345/api/Korisnik/getAllSlucajForKorisnik');
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
  postavljanjeNoveCeneOdKorisnika(slucajAdvokat) {
    return this.http.put(` http://localhost:44345/api/Korisnik/postavljanjeNoveCeneOdKorisnika`, slucajAdvokat).subscribe(res => {

    })
  }
  prepravkaSlucajaKorisnika(slucajAdvokat) {
    return this.http.put('http://localhost:44345/api/Korisnik/prepravkaSlucajaKorisnik', slucajAdvokat).subscribe(res => {
      console.log(res);
    })
  }
  getAllKategorije() {
    return this.http.get('http://localhost:44345/api/Korisnik/getAllKategorije');
  }
  postSlucajAdvokatima(slucaj){
    return this.http.post('http://localhost:44345/api/Korisnik/postSlucajAdvokatima', slucaj).subscribe(rez => {
    });
  }
  postRequestAdvokats(advokati){
    return this.http.post('http://localhost:44345/api/Korisnik/postRequestAdvokats', advokati).subscribe();
  }
  GetAllSlucajAdvokatForKorisnik() {
    return this.http.get('http://localhost:44345/api/Korisnik/getAllSlucajAdvokatForKorisnik');
  }
  prihvacenSlucajOdKorisnika (slucaj) {
    return this.http.put ('http://localhost:44345/api/Korisnik/prihvacenSlucajKorisnik', slucaj);
  }
  odbijenSlucajOdKorisnika(slucaj) {
    return this.http.put('http://localhost:44345/api/Korisnik/odbijenSlucajOdKorisnika', slucaj).subscribe(res => {
      console.log(res)
    });
  }
  getNewNostifiation() {
    return this.http.get('http://localhost:44345/api/Korisnik/getNewNostifiation');
  }
  resetNotification() {
    return this.http.put('http://localhost:44345/api/Korisnik/putNewNostifiationReadKorisnik', null);
  }
  getSlucajById(id) {
    return this.http.get('http://localhost:44345/api/Korisnik/GetSlucajById'+ "/" + id)
  }
  upload(file: any) {
    return this.http.post('http://localhost:44345/api/Korisnik/uploadFile', file).subscribe();
  }
  editSlucaj(slucaj) {
    return this.http.put('http://localhost:44345/api/Korisnik/editSlucaj', slucaj).subscribe();
  }
}
