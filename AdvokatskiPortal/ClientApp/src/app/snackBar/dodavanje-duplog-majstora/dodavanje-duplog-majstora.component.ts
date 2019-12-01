import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material';

@Component({
  selector: 'app-dodavanje-duplog-majstora',
  templateUrl: './dodavanje-duplog-majstora.component.html',
  styleUrls: ['./dodavanje-duplog-majstora.component.css']
})
export class DodavanjeDuplogMajstoraComponent implements OnInit {
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {}

  ngOnInit() {
  }

}
