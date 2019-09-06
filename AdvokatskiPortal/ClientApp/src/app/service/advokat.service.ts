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
  getUgovorsForAdvokat(){
    return this.http.get<any[]>(" http://localhost:44345/api/Advokat/getUgovorsForAdvokat");
  }
  getSlucajiNaCekanju() {
    return this.http.get(" http://localhost:44345/api/Advokat/getSlucajNaCekanju");
  }
  getSlucajiPrihvaceni(){
    return this.http.get(" http://localhost:44345/api/Advokat/getSlucajiPrihvaceni" );
  }
  prihvacenSlucajAdvokat(slucajAdvokat) {
    return this.http.put(` http://localhost:44345/api/Advokat/prihvacenSlucajAdvokat/${slucajAdvokat.advokatId}`, slucajAdvokat);
  }
}
