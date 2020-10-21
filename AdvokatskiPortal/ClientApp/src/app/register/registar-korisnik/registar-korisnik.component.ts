import { AuthService } from './../../service/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-registar-korisnik',
  templateUrl: './registar-korisnik.component.html',
  styleUrls: ['./registar-korisnik.component.css']
})
export class RegistarKorisnikComponent implements OnInit {
  form: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.form = this.fb.group({
      FirstName: [ '' , Validators.required ],
      LastName: [ '' , Validators.required ],
      UserName: [ '' , Validators.required ],
      Password: [ '' , Validators.required ],
      Email: [ '' , Validators.required ],
      Place: [ '' , Validators.required ],
      Street: [ '' , Validators.required ]
    });
  }

  ngOnInit() {
  }

}
