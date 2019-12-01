import { element } from 'protractor';
import { MajstorRegistarVM } from '../../model/MajstorRegistarVM'
import { Majstor } from '../../model/majstor'
import { MajstorService } from '../../service/majstor.service'
import { AuthService } from '../../service/auth.service';

import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registracija-majstora',
  templateUrl: './registracija-majstora.component.html',
  styleUrls: ['./registracija-majstora.component.css']
})
export class RegistracijaMajstoraComponent implements OnInit {
  form: FormGroup;
  majstor: Majstor = new Majstor() ;
  originalData;
  kategorije;
  podKategorije;
  //majstor: MajstorRegistarVM = new MajstorRegistarVM;
  constructor( private authService: AuthService, private majstorService: MajstorService) {

  }

  ngOnInit() {
   this.getAllCategories();
  }
  save() {
    // this.majstor.Kategorije = this.podKategorije;
    this.authService.registrationMajstor(this.majstor);
    // console.log(this.majstor)
  }
  private getAllCategories() {
    this.majstorService.getAllKategorija().subscribe((res: any) => {
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
