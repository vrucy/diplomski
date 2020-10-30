import { element } from 'protractor';
import { CraftmanService } from '../../../service/Craftman.service'
import { Contract } from '../../../model/Contrct';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-modificartion-offer',
  templateUrl: './modificartion-offer.component.html',
  styleUrls: ['./modificartion-offer.component.css']
})
export class ModificationOfferComponent implements OnInit {
  public test: any;
  Contract
  private submitCallback: Function;
  hideUserOptions: boolean;

  constructor(private craftmanService: CraftmanService,
    public dialogRef: MatDialogRef<ModificationOfferComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.Contract = data.contract;
    console.log(data)
    this.Contract.startDate = data.startDate;
    this.Contract.finishDate = data.finishDate;
    this.submitCallback = data.submitCallback;
    this.hideUserOptions = data.hideUserOptions;
    console.log(this.Contract)
  }

  ngOnInit() {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onYesClick(): void {
    if (this.submitCallback) {
      const Contract = {...this.Contract};
      Contract.case.pictures.forEach((picture: any) => {
        if (picture.pictureBytes) {
         const base64result = picture.pictureBytes.split(',')[1];
         picture.pictureBytes = base64result;
        }
      });
      Contract.price = this.data.price;
      Contract.typeOfPayment = this.data.typeOfPayment;
      Contract.StartDate = this.data.startDate;
      Contract.finishDate = this.data.finishDate;
      Contract.comment = this.data.comment;
      this.submitCallback(Contract);
    }
  }

}
