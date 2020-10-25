import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material';

@Component({
  selector: 'app-add-double-craftman',
  templateUrl: './add-double-craftman.component.html',
  styleUrls: ['./add-double-craftman.component.css']
})
export class AddDoubleCraftmanComponent implements OnInit {
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {}

  ngOnInit() {
  }

}
