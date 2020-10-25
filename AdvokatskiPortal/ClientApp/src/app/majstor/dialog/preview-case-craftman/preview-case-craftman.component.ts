import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { dialogPrikazSlucaja } from '../../../model/dialogPrikazSlucaja';

@Component({
  selector: 'app-preview-case-craftman',
  templateUrl: './preview-case-craftman.component.html',
  styleUrls: ['./preview-case-craftman.component.css']
})
export class PreviewCaseCraftmanComponent {

  images: any[] = [];

  constructor( public dialogRef: MatDialogRef<PreviewCaseCraftmanComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.images = data.pictures.map((s) => s.pictureBytes);
    }

    close(): void {
      this.dialogRef.close();
    }

}
