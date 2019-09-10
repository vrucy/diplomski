import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-prepraviti-ponudu',
  templateUrl: './prepraviti-ponudu.component.html',
  styleUrls: ['./prepraviti-ponudu.component.css']
})
export class PrepravitiPonuduComponent {

  constructor(
    public dialogRef: MatDialogRef<PrepravitiPonuduComponent>,
    @Inject(MAT_DIALOG_DATA) public data: dialogPrepravljeniPodaci) {}

    onNoClick(): void {
    this.dialogRef.close();
  }

}
