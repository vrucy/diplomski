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
  majstor: Majstor = new Majstor() ;
  originalData;
  kategorije;
  podKategorije;
  //majstor: MajstorRegistarVM = new MajstorRegistarVM;
  constructor( private authService: AuthService, private advokatService: AdvokatService) {
    
  }

  ngOnInit() {
   this.getAllCategories();
  }
  save(){
    this.authService.registrationAdvokat(this.majstor);
    //console.log(this.majstor)
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
