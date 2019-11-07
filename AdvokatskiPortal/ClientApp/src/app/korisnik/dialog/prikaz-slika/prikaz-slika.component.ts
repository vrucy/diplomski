import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-prikaz-slika',
  templateUrl: './prikaz-slika.component.html',
  styleUrls: ['./prikaz-slika.component.css']
})
export class PrikazSlikaComponent {

  images: any[] = [];

  constructor( public dialogRef: MatDialogRef<PrikazSlikaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.images = data.slike.map((s) => s.slikaProp);
    }

    close(): void {
      this.dialogRef.close();
    }

}
