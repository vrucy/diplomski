import { Category } from '../../model/Category';
import { CraftmanService } from '../../service/Craftman.service'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-sub-category',
  templateUrl: './add-sub-category.component.html',
  styleUrls: ['./add-sub-category.component.css']
})
export class AddSubCategoryComponent implements OnInit {
  form: FormGroup;
  categories: Category;
  constructor(private fb: FormBuilder, private craftmanService: CraftmanService) {
    this.form = this.fb.group({
      ParentId: ['', Validators.required],
      Name: ['' , Validators.required ],
      Description: ['' , Validators.required ]
    });
   }

  ngOnInit() {
    this.craftmanService.GetAllCategories().subscribe((res: any) => {
      this.categories = res.filter(x => !x.parentId);
    });
  }

}
