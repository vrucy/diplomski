import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-edit-profila',
  templateUrl: './edit-profila.component.html',
  styleUrls: ['./edit-profila.component.css']
})
export class EditProfilaComponent implements OnInit {
  trenutniKorinsik = {};
  constructor(private fb: FormBuilder ) { 
  this.trenutniKorinsik = localStorage.getItem('trenutniKorisnik');
  console.log(this.trenutniKorinsik)
  }
 
  ngOnInit() {
    //console.log(this.trenutniKorinsik)

    }

}
