import { element } from 'protractor';
import { MajstorRegistarVM } from './../../model/MajstorRegistarVM';
import { Majstor } from './../../model/Majstor';
import { AdvokatService } from './../../service/advokat.service';
import { AuthService } from '../../service/auth.service';

import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registracija-advokata',
  templateUrl: './registracija-advokata.component.html',
  styleUrls: ['./registracija-advokata.component.css']
})
export class RegistracijaAdvokataComponent implements OnInit {
  form: FormGroup;
  originalData;
  kategorije;
  podKategorije;
  majstor: MajstorRegistarVM = new MajstorRegistarVM;
  constructor(private fb: FormBuilder, private authService: AuthService, private advokatService: AdvokatService) {
    this.form = this.fb.group({
      Ime: ['', Validators.required],
      Prezime: ['', Validators.required],
      UserName: ['', Validators.required],
      Password: ['', Validators.required],
      Email: ['', Validators.required],
      Mesto: ['', Validators.required],
      Ulica: ['', Validators.required],
      kategorijaId: ['', Validators.required],
      podKategorijaId: ['', Validators.required ]
    });
  }

  ngOnInit() {
   this.getAllCategories();
  }

  private getAllCategories() {
    this.advokatService.getAllKategorija().subscribe((res: any) => {
      this.originalData = res;
      this.kategorije = [...res].filter(x => !x.parentId);
    });
  }

  onParentChanged(evt) {
    this.setSubcategories(evt.value);
  }

  setSubcategories(parentId) {
    this.podKategorije =  [...this.originalData].filter(x => x.parentId === parentId);
  }



}
