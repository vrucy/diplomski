import { Advokat } from './../model/Advokat';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KorisnikService {

  constructor(private http: HttpClient) { }
  getAllAdvokati(): Observable<Advokat[]>{
    return this.http.get<Advokat[]>('http://localhost:44345/api/Advokats');
}
}
