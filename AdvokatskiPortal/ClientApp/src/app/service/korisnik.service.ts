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
  getAllMajstori(){
    return this.http.get('http://localhost:44345/api/Korisnik/getAllMajstori');
  }
  getAllSlucajForKorisnik() {
    return this.http.get ('http://localhost:44345/api/Korisnik/getAllSlucajForKorisnik');
  }
  kreiranjeSlucaja(slucaj){
    return this.http.post('http://localhost:44345/api/Korisnik/kreiranjeSlucaja', slucaj);
  }
  postSlucajMajstorima(slucaj){
    return this.http.post('http://localhost:44345/api/Korisnik/postSlucajMajstorima', slucaj).subscribe()
  }
  postavljanjeNoveCeneOdKorisnika(slucajMajstor) {
    return this.http.put(` http://localhost:44345/api/Korisnik/postavljanjeNoveCeneOdKorisnika`, slucajMajstor)
  }
  // prepravkaSlucajaKorisnika(slucajMajstor) {
  //   return this.http.put('http://localhost:44345/api/Korisnik/prepravkaSlucajaKorisnik', slucajMajstor).subscribe()
  // }
  getAllKategorije() {
    return this.http.get('http://localhost:44345/api/Korisnik/getAllKategorije');
  }
  // postSlucajMajstorima(slucaj){
  //   return this.http.post('http://localhost:44345/api/Korisnik/postSlucajMajstorima', slucaj).subscribe(rez => {
  //   });
  // }
  // postRequestMajstors(majstori){
  //   return this.http.post('http://localhost:44345/api/Korisnik/postRequestMajstors', majstori).subscribe();
  // }
  GetAllSlucajMajstorForKorisnik() {
    return this.http.get('http://localhost:44345/api/Korisnik/getAllSlucajMajstorForKorisnik');
  }
  prihvacenSlucajOdKorisnika (slucaj) {
    return this.http.put ('http://localhost:44345/api/Korisnik/prihvacenSlucajKorisnik', slucaj);
  }
  odbijenSlucajOdKorisnika(ids) {
    return this.http.put('http://localhost:44345/api/Korisnik/odbijenSlucajOdKorisnika', ids).subscribe(res => {
      console.log(res)
    });
  }
  // getNewNostifiation() {
  //   return this.http.get('http://localhost:44345/api/Korisnik/getNewNostifiation');
  // }
  // resetNotification() {
  //   return this.http.put('http://localhost:44345/api/Korisnik/putNewNostifiationReadKorisnik', null);
  // }
  getSlucajById(id) {
    return this.http.get(`http://localhost:44345/api/Korisnik/GetSlucajById/` + id);
  }
  upload(file: any) {
    return this.http.post('http://localhost:44345/api/Korisnik/uploadFile', file).subscribe();
  }
  editSlucaj(slucaj) {
    return this.http.put('http://localhost:44345/api/Korisnik/editSlucaj', slucaj);
  }
}
