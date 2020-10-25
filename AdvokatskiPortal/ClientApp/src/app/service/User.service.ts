import { Craftman } from '../model/Craftman'
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Case } from '../model/Case';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  GetAllCraftmans(){
    return this.http.get('http://localhost:44345/api/User/GetAllCraftmans');
  }
  GetAllCaseForUser() {
    return this.http.get ('http://localhost:44345/api/User/GetAllCaseForUser');
  }
  CreateCase(slucaj){
    return this.http.post('http://localhost:44345/api/User/CreateCase', slucaj);
  }
  PostCaseCraftmans(slucaj){
    return this.http.post('http://localhost:44345/api/User/PostCaseCraftmanima', slucaj)
  }
  PostNewPriceFromUser(slucajMajstor) {
    return this.http.put(` http://localhost:44345/api/User/PostNewPriceFromUser`, slucajMajstor)
  }
  // prepravkaSlucajaKorisnika(slucajMajstor) {
  //   return this.http.put('http://localhost:44345/api/Korisnik/prepravkaSlucajaKorisnik', slucajMajstor).subscribe()
  // }
  GetAllCategories() {
    return this.http.get('http://localhost:44345/api/User/GetAllCategories');
  }
  // postSlucajMajstorima(slucaj){
  //   return this.http.post('http://localhost:44345/api/Korisnik/postSlucajMajstorima', slucaj).subscribe(rez => {
  //   });
  // }
  // postRequestMajstors(majstori){
  //   return this.http.post('http://localhost:44345/api/Korisnik/postRequestMajstors', majstori).subscribe();
  // }
  GetAllCaseCraftmanForUser() {
    return this.http.get('http://localhost:44345/api/User/GetAllCaseCraftmanForUser');
  }
  AcceptedCaseFromUser (slucaj) {
    return this.http.put ('http://localhost:44345/api/User/AcceptedCaseFromUser', slucaj);
  }
  RejectCaseFromUser(ids) {
    return this.http.put('http://localhost:44345/api/User/RejectCaseFromUser', ids).subscribe(res => {
      console.log(res)
    });
  }
  GetNewNostifiation() {
    return this.http.get('http://localhost:44345/api/User/GetNewNostifiation');
  }
  // resetNotification() {
  //   return this.http.put('http://localhost:44345/api/Korisnik/putNewNostifiationReadKorisnik', null);
  // }
  getCaseById(id) {
    return this.http.get(`http://localhost:44345/api/User/GetCaseById/` + id);
  }
  upload(file: any) {
    return this.http.post('http://localhost:44345/api/User/uploadFile', file).subscribe();
  }
  EditCase(slucaj) {
    return this.http.put('http://localhost:44345/api/User/EditCase', slucaj);
  }
}
