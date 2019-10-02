import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { dialogPrikazSlucaja } from '../../../model/dialogPrikazSlucaja';

@Component({
  selector: 'app-prikaz-slucaj',
  templateUrl: './prikaz-slucaj.component.html',
  styleUrls: ['./prikaz-slucaj.component.css']
})
export class PrikazSlucajComponent {
  url = '';

  constructor( public dialogRef: MatDialogRef<PrikazSlucajComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

    close(): void {
    this.dialogRef.close();
  }
}
