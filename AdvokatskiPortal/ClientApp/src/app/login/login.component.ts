import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  form: FormGroup;
  public regular: string ;
  public admin: string ;
  constructor(private fb: FormBuilder ) {
    this.form = this.fb.group({
      UserName:['' , Validators.required ],
      Password:['' , Validators.required ]
    })
    
   }
 
  ngOnInit() {
    
  }
 
 
  
}
