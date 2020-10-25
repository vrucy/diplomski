import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-preview-case-user',
  templateUrl: './preview-case-user.component.html',
  styleUrls: ['./preview-case-user.component.css']
})
export class PreviewCaseUserComponent {
  images: any[] = [];
  constructor( public dialogRef: MatDialogRef<PreviewCaseUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }
    // console.log(data.slike);
    // this.images = data.slike.map((s) => s.slikaProp);
    // console.log(this.images);

    close(): void {
    this.dialogRef.close();
  }
 }
