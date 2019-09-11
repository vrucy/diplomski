import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdvokatService {

  constructor(private http: HttpClient) { }

  getNewNostifiation() {
    return this.http.get( " http://localhost:44345/api/Advokat/getNewNostifiation");
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
  prihvatanjeSlucajaOdAdvokata(slucajAdvokat) {
    return this.http.put(` http://localhost:44345/api/Advokat/prihvatanjeSlucajaAdvokata`, slucajAdvokat);
  }
  odbijanjeSlucajaOdAdvokata(slucajAdvokat) {
    return this.http.put(` http://localhost:44345/api/Advokat/odbijanjeSlucajaAdvokata`, slucajAdvokat);
  }
}
