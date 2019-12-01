import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MajstorService } from '../../service/majstor.service';

@Component({
  selector: 'app-dodaj-kategoriju',
  templateUrl: './dodaj-kategoriju.component.html',
  styleUrls: ['./dodaj-kategoriju.component.css']
})
export class DodajKategorijuComponent implements OnInit {
  form: FormGroup;

  constructor(private fb: FormBuilder, private majstorService: MajstorService) {
    this.form = this.fb.group({
      Naziv: ['' , Validators.required ],
      Opis: ['' , Validators.required ]
    });
  }

  ngOnInit() {
  }

}
