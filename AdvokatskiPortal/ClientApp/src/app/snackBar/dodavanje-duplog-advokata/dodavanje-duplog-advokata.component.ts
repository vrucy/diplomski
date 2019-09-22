import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material';

@Component({
  selector: 'app-dodavanje-duplog-advokata',
  templateUrl: './dodavanje-duplog-advokata.component.html',
  styleUrls: ['./dodavanje-duplog-advokata.component.css']
})
export class DodavanjeDuplogAdvokataComponent implements OnInit {
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {}

  ngOnInit() {
  }

}
