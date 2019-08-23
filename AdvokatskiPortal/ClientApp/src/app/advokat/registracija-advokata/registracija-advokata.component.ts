import { AuthService } from '../../service/auth.service';

import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registracija-advokata',
  templateUrl: './registracija-advokata.component.html',
  styleUrls: ['./registracija-advokata.component.css']
})
export class RegistracijaAdvokataComponent implements OnInit {
  form;
  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.form = this.fb.group({
      Ime: [ '' , Validators.required ],
      Prezime: [ '' , Validators.required ],
      UserName: [ '' , Validators.required ],
      Password: [ '' , Validators.required ],
      Email: [ '' , Validators.required ],
      Mesto: [ '' , Validators.required ],
      Ulica: [ '' , Validators.required ]
    });
   }

  ngOnInit() {
  }

}
