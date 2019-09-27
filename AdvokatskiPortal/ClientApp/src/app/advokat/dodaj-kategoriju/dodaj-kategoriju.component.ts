import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdvokatService } from '../../service/advokat.service';

@Component({
  selector: 'app-dodaj-kategoriju',
  templateUrl: './dodaj-kategoriju.component.html',
  styleUrls: ['./dodaj-kategoriju.component.css']
})
export class DodajKategorijuComponent implements OnInit {
  form: FormGroup;

  constructor(private fb: FormBuilder, private advokatService: AdvokatService) {
    this.form = this.fb.group({
      Naziv: ['' , Validators.required ],
      Opis: ['' , Validators.required ]
    });
  }

  ngOnInit() {
  }

}
