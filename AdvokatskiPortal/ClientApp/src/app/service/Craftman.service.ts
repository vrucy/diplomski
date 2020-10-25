import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CraftmanService {

  constructor(private http: HttpClient) { }

  GetNewNostification() {
    return this.http.get( "http://localhost:44345/api/Craftman/GetNewNostification");
  }

  GetContractFromCraftmans(){
    return this.http.get<any[]>(" http://localhost:44345/api/Craftman/GetContractFromCraftmans");
  }
  
  NewPriceFromCraftman(slucajMajstor) {
    return this.http.post(` http://localhost:44345/api/Craftman/NewPriceFromCraftman`, slucajMajstor).subscribe(res =>{
      console.log(res)
    });
  }
  
  ModificationCaseFromCraftman(slucajMajstor) {
    return this.http.put('http://localhost:44345/api/Craftman/ModificationCaseFromCraftman', slucajMajstor);
  }

  AcceptCaseFromCraftman(slucajMajstor) {
    return this.http.put(` http://localhost:44345/api/Craftman/AcceptCaseFromCraftman`, slucajMajstor);
  }

  RejectCaseFromCraftman(slucajMajstor) {
    return this.http.put(' http://localhost:44345/api/Craftman/RejectCaseFromCraftman', slucajMajstor);
  }

  PostCategory(kategorija) {
    return this.http.post('http://localhost:44345/api/Craftman/PostCategory', kategorija).subscribe(res => {
      console.log(res);
    });
  }

  GetAllCategories() {
    return this.http.get('http://localhost:44345/api/Craftman/GetAllCategories');
  }
  
  PostSubCategory(subCategory) {
    return this.http.post('http://localhost:44345/api/Craftman/PostSubCategory', subCategory).subscribe(res => {
      console.log(res);
    });
  }

}
