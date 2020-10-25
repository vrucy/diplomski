
import { Craftman } from '../../model/Craftman'
import { CraftmanService } from '../../service/Craftman.service'
import { AuthService } from '../../service/auth.service';

import {  FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration-craftman',
  templateUrl: './registration-craftman.component.html',
  styleUrls: ['./registration-craftman.component.css']
})
export class RegistrationCraftmanComponent implements OnInit {
  form: FormGroup;
  craftman: Craftman = new Craftman() ;
  originalData;
  categories;
  subCategories;
  //majstor: MajstorRegistarVM = new MajstorRegistarVM;
  constructor( private authService: AuthService, private craftmanService: CraftmanService) {

  }

  ngOnInit() {
   this.getAllCategories();
  }
  save() {
    // this.majstor.Kategorije = this.podKategorije;
    this.authService.registrationCraftman(this.craftman);
    // console.log(this.majstor)
  }
  private getAllCategories() {
    this.craftmanService.GetAllCategories().subscribe((res: any) => {
      this.originalData = res;
      this.categories = [...res].filter(x => !x.parentId);
    });
  }

  onParentChanged(evt) {
    this.setSubcategories(evt.value);
  }

  setSubcategories(parentId) {
    this.subCategories =  [...this.originalData].filter(x => x.parentId === parentId);
  }



}
