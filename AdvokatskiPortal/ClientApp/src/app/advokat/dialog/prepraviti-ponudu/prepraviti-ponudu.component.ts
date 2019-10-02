import { AdvokatService } from './../../../service/advokat.service';
import { Cenovnik } from './../../../model/Cenovnik';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-prepraviti-ponudu',
  templateUrl: './prepraviti-ponudu.component.html',
  styleUrls: ['./prepraviti-ponudu.component.css']
})
export class PrepravitiPonuduComponent implements OnInit{


  public test: any;


  constructor( private advokatService: AdvokatService,
    public dialogRef: MatDialogRef<PrepravitiPonuduComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any ) {}

    ngOnInit() {

    }
    makeCenovnki() {
      
    }
    onNoClick(): void {
    this.dialogRef.close();
  }

}
