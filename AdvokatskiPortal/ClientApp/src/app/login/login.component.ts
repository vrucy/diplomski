import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  form: FormGroup;
  role : string;
  
  constructor(private fb: FormBuilder, public auth:AuthService ) {
    this.form = this.fb.group({
      UserName:['' , Validators.required ],
      Password:['' , Validators.required ]
    })  
   }
   
   private httpOptions = {
    headers: new HttpHeaders({
        'Accept': 'application/text'
    }),
    responseType: 'text'
}
 
  ngOnInit() {
    this.role=this.readLocalvariable('typeUser');
    localStorage.clear();
  }
 
  readLocalvariable(typeUser:string){ 
    return localStorage.getItem(typeUser)   
  }
  
}
