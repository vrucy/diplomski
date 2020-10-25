import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-show-pictures',
  templateUrl: './show-pictures.component.html',
  styleUrls: ['./show-pictures.component.css']
})
export class ShowPicturesComponent {

  images: any[] = [];

  constructor( public dialogRef: MatDialogRef<ShowPicturesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.images = data.pictures.map((s) => s.pictureBytes);
    }

    close(): void {
      this.dialogRef.close();
    }

}
