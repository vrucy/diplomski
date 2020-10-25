import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CraftmanService } from '../../service/Craftman.service'

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {
  form: FormGroup;

  constructor(private fb: FormBuilder, private craftmanService: CraftmanService) {
    this.form = this.fb.group({
      Name: ['' , Validators.required ],
      Descrition: ['' , Validators.required ]
    });
  }

  ngOnInit() {
  }

}
