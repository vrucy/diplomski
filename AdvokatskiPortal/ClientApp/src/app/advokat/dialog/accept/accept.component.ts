import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { dialogPrihcvacenPodaci } from '../../../model/dialogPrihvacenPodaci';

@Component({
  selector: 'app-accept',
  templateUrl: './accept.component.html',
  styleUrls: ['./accept.component.css']
})
export class AcceptComponent {

  constructor(
    public dialogRef: MatDialogRef<AcceptComponent>,
    @Inject(MAT_DIALOG_DATA) public data: dialogPrihcvacenPodaci) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}
