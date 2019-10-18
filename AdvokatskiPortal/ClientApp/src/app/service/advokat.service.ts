import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdvokatService {

  constructor(private http: HttpClient) { }

  getNewNostifiation() {
    return this.http.get( "http://localhost:44345/api/Advokat/getNewNostifiation");
  }
  putNostificationRead(nostifiation){
    return this.http.put('http://localhost:44345/api/Advokat/putNewNostifiationRead', nostifiation);
  }
  getNaciniPlacanja() {
    return this.http.get(" http://localhost:44345/api/Advokat/getNacinPlacanja");
  }
  getUgovorsForAdvokat(){
    return this.http.get<any[]>(" http://localhost:44345/api/Advokat/getUgovorsForAdvokat");
  }
  getSlucajiNaCekanju() {
    return this.http.get(" http://localhost:44345/api/Advokat/getSlucajNaCekanju");
  }
  getSlucajiPrihvaceni(){
    return this.http.get(" http://localhost:44345/api/Advokat/getSlucajiPrihvaceni" );
  }
  postavljanjeNoveCeneOdAdvokata(slucajAdvokat) {
    return this.http.post(` http://localhost:44345/api/Advokat/postavljanjeNoveCeneOdAdvokata`, slucajAdvokat).subscribe(res =>{
      console.log(res)
    });
  }
  prepravkaCeneOdAdvokata(cenovnik) {
    return this.http.put(` http://localhost:44345/api/Advokat/prepravkaCeneOdAdvokata`, cenovnik).subscribe(res =>{
      console.log(res);
    });
  }
  prepravkaSlucajaAdvokata(slucajAdvokat) {
    return this.http.put('http://localhost:44345/api/Advokat/prepravkaSlucajaAdvokata', slucajAdvokat).subscribe(res => {
      console.log(res)
    })
  }
  prihvatanjeSlucajaOdAdvokata(slucajMajstor) {
    return this.http.put(` http://localhost:44345/api/Advokat/prihvatanjeSlucajaAdvokata`, slucajMajstor);
  }
  odbijanjeSlucajaOdAdvokata(slucajAdvokat) {
    return this.http.put(' http://localhost:44345/api/Advokat/odbijanjeSlucajaAdvokata', slucajAdvokat);
  }
  postKategorija(kategorija) {
    return this.http.post('http://localhost:44345/api/Advokat/postKategorija', kategorija).subscribe(res => {
      console.log(res);
    });
  }
  getAllKategorija() {
    return this.http.get('http://localhost:44345/api/Advokat/getAllKategorija');
  }
  postPodKategorija(podKategorija) {
    return this.http.post('http://localhost:44345/api/Advokat/postPodKategorija', podKategorija).subscribe(res => {
      console.log(res);
    });
  }

}
