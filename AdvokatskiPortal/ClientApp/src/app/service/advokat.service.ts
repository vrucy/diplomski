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
    return this.http.get( " http://localhost:44345/api/Advokat/getUgovorsForAdvokat");
  }
}
