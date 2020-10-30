import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material';

@Component({
  selector: 'app-error-writer',
  templateUrl: './error-writer.component.html',
  styleUrls: ['./error-writer.component.css']
})
export class ErrorWriterComponent implements OnInit {
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {}

  ngOnInit() {
  }

}
