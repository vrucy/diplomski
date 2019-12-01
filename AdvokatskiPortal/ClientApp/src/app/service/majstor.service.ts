import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MajstorService {

  constructor(private http: HttpClient) { }

  getNewNostifiation() {
    return this.http.get( "http://localhost:44345/api/Majstor/getNewNostifiation");
  }
  // putNostificationRead(nostifiation){
  //   return this.http.put('http://localhost:44345/api/Majstor/putNewNostifiationRead', nostifiation);
  // }
  // getNaciniPlacanja() {
  //   return this.http.get(" http://localhost:44345/api/Majstor/getNacinPlacanja");
  // }
  getUgovorsForMajstor(){
    return this.http.get<any[]>(" http://localhost:44345/api/Majstor/getUgovorsForMajstor");
  }
  // getSlucajiNaCekanju() {
  //   return this.http.get(" http://localhost:44345/api/Majstor/getSlucajNaCekanju");
  // }
  // getSlucajiPrihvaceni(){
  //   return this.http.get(" http://localhost:44345/api/Majstor/getSlucajiPrihvaceni" );
  // }
  postavljanjeNoveCeneOdMajstora(slucajMajstor) {
    return this.http.post(` http://localhost:44345/api/Majstor/postavljanjeNoveCeneOdMajstora`, slucajMajstor).subscribe(res =>{
      console.log(res)
    });
  }
  // prepravkaCeneOdMajstora(cenovnik) {
  //   return this.http.put(` http://localhost:44345/api/Majstor/prepravkaCeneOdMajstora`, cenovnik).subscribe(res =>{
  //     console.log(res);
  //   });
  // }
  prepravkaSlucajaMajstora(slucajMajstor) {
    return this.http.put('http://localhost:44345/api/Majstor/prepravkaSlucajaMajstora', slucajMajstor);
  }
  prihvatanjeSlucajaOdMajstora(slucajMajstor) {
    return this.http.put(` http://localhost:44345/api/Majstor/prihvatanjeSlucajaMajstora`, slucajMajstor);
  }
  odbijanjeSlucajaOdMajstora(slucajMajstor) {
    return this.http.put(' http://localhost:44345/api/Majstor/odbijanjeSlucajaMajstora', slucajMajstor);
  }
  postKategorija(kategorija) {
    return this.http.post('http://localhost:44345/api/Majstor/postKategorija', kategorija).subscribe(res => {
      console.log(res);
    });
  }
  getAllKategorija() {
    return this.http.get('http://localhost:44345/api/Majstor/getAllKategorija');
  }
  postPodKategorija(podKategorija) {
    return this.http.post('http://localhost:44345/api/Majstor/postPodKategorija', podKategorija).subscribe(res => {
      console.log(res);
    });
  }

}
