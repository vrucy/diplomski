import { Kategorija } from '../../model/Kategorija';
import { MajstorService } from '../../service/majstor.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dodaj-pod-kategoriju',
  templateUrl: './dodaj-pod-kategoriju.component.html',
  styleUrls: ['./dodaj-pod-kategoriju.component.css']
})
export class DodajPodKategorijuComponent implements OnInit {
  form: FormGroup;
  kategorije: Kategorija;
  constructor(private fb: FormBuilder, private majstorService: MajstorService) {
    this.form = this.fb.group({
      ParentId: ['', Validators.required],
      Naziv: ['' , Validators.required ],
      Opis: ['' , Validators.required ]
    });
   }

  ngOnInit() {
    this.majstorService.getAllKategorija().subscribe((res: any) => {
      this.kategorije = res.filter(x => !x.parentId);
      console.log(this.kategorije)
    });
  }

}
