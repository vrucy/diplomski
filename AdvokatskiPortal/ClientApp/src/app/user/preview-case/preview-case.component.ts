import { PreviewCaseCraftmanComponent } from '../../craftman/dialog/preview-case-craftman/preview-case-craftman.component';
import { ModificationOfferComponent } from '../../craftman/dialog/modificartion-offer/modificartion-offer.component';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from '../../service/User.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource, MatPaginator, Sort } from '@angular/material';
import { FormControl } from '@angular/forms';
import { Contract } from '../../model/Contrct';

import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-preview-case',
  templateUrl: './preview-case.component.html',
  styleUrls: ['./preview-case.component.css']
})
export class PreviewCaseComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['FirstName', 'LastName', 'TypeOfPayment', 'Price', 'CreationDate'
    , 'StartDate', 'FinishDate','DateRecive', 'ChangeCaseDate', 'Name', 'button'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  allCases: any;
  allCasesSelected;
  private _filterInputValue: string;
  public set filterInputValue(val: any) {
    this._filterInputValue = val;
    this.filterData();
  }
  public get filterInputValue() {
    return this._filterInputValue;
  }
  private cachedData: any[];
  private filteredData: any[];
  private _caseValue: any;
  public set caseValue(val: any) {
    this._caseValue = val;
    this.filterData();
  }
  public get caseValue() {
    return this._caseValue;
  }
  odgovor: any;

  constructor(private userService: UserService, public dialog: MatDialog) {
    this.initialize();
  }
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngAfterViewInit() {
    this.dataSource.sortingDataAccessor = (item: any, property: any) => {
      switch (property) {
        case 'startDate': {
          const newDate = new Date(item.contracts.startDate);
          return newDate;
        }
        case 'finishDate': {
          const newDate = new Date(item.contracts.finishDate);
          return newDate;
        }
        case 'creationDate': {
          const newDate = new Date(item.contracts.creationDate );
          return newDate;
        }
        case 'name': {
          return item.case.name;
        }
        case 'kolicina': {
          return item.contracts.price;
        }
        case 'typeOfPayment': {
          return item.contracts.typeOfPayment;
        }
        default: {
          return item[property];
        }
      }
    };

  }
  initialize() {
    this.userService.GetAllCaseCraftmanForUser().subscribe((res: any) => {
      this.allCases = res;
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;
      console.log(this.filteredData)
      this.remapImagesForDisplay(res); 
      this.handleTabChange(0);
    });
  }
  submitPopupForm(result) {
    // this.handleSubmitData(result);
    this.userService.PostNewPriceFromUser(result).subscribe(res => {
      this.initialize(); 
    });
  }

  // private handleSubmitData(result) {
  //   this.userService.postavljanjeNoveCeneOdKorisnika(result).subscribe(res => {
  //     this.initialize();
  //   });
  // }
  openDialogEdit(element): void {
    const dialogRef = this.dialog.open(ModificationOfferComponent, {
      width: '250px',
      data: {
        contract: Object.assign(new Contract(), element),
        submitCallback: this.submitPopupForm.bind(this),
        hideUserOptions: false
      }
    });

  }
  openDialogPreviewCase(element): void {    
    const dialogRef = this.dialog.open(PreviewCaseCraftmanComponent, {
      maxWidth: '40%',
      maxHeight: '70%',
      data: { name: element.case.name, description: element.case.description, pictures: element.case.pictures,
             comment: element.contracts.comment }
    });
    dialogRef.afterClosed().subscribe(result => {
      //ne treba, proveriti jos
      element.odgovor = result;
      console.log(element.odgovor)
      this.odgovor = result;
    });
  }
  private remapImagesForDisplay(data) {
    data.forEach(c => {
      const baseSlike = c.case.pictures.map(s => {
        s.pictureBytes = 'data:image/jpeg;base64,' + s.pictureBytes;
        return s;
      });
    });
  }
  filter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  removeAt(index: number) {
    const data = this.dataSource.data;
    data.splice(index, 1);

    this.dataSource.data = data;
  }
  acceptCase(c) {
    const ids = { craftmanId: c.craftmanId, caseId: c.case.id }
    this.userService.AcceptedCaseFromUser(ids).subscribe(res => {
      //this.removeAt(slucaj);
    });
  }
  rejectCase(c) {
    const ids = { craftmanId: c.craftmanId, caseId: c.case.id }
    console.log(ids)
    this.userService.RejectCaseFromUser(ids);
  }

  handleButton(element) {
    switch (element.caseStatusId) {
      case 1:
        return 'Waiting answer from craftman';
        break;
      case 3:
        return 'Odbili ste ovu ponudu';
        break;
      case 5:
        return 'Odbijena ponuda majstora';
        break;
      case 2:
        return 'Prihvatili ste ovu ponudu';
        break;
      case 6:
        return 'Čeka se odgovor majstora';
        break;
      default:
        break;
    }
  }
  handleSlucaj(ids) {
    this.allCasesSelected = [];
    ids.forEach(el => {
      const x = [...this.filteredData].find(sso => sso.case.id === el);
      this.allCasesSelected = [...this.allCasesSelected, x.case];
    });
  }
  handleTabChange(tab) {
    switch (tab) {
      // svi
      case 0:
        //  this.resetFilter();
        const unique0 = [...new Set(this.filteredData.map(item => item.case.id))];
        this.handleSlucaj(unique0);
        this.dataSource.data = [...this.filteredData];
        break;
      // filter prihvaceni
      case 1:
        // this.resetFilter();
        const unique = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 2).map(item => item.case.id))];
        console.log(unique)
        this.handleSlucaj(unique);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 2);
        break;
      // filter u procesu
      case 2:
        // this.resetFilter();
        const unique1 = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 4 ||
          ss.caseStatusId === 7 || ss.caseStatusId === 1 || ss.caseStatusId === 6).map(item => item.case.id))];
        console.log(unique1);
        this.handleSlucaj(unique1);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 4 ||
          ss.caseStatusId === 7 || ss.caseStatusId === 1 || ss.caseStatusId === 6);
        break;
      // filter odbijeni
      case 3:
        const unique2 = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 5).map(item => item.case.id))];
        this.handleSlucaj(unique2);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 5);
        break;
      case 4:
        const unique3 = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 3).map(item => item.case.id))];
        this.handleSlucaj(unique3);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 3);
        break;
      default:
        break;
    }
  }
  resetFilter() {
    this.filterInputValue = null;
    this.caseValue = null;
    this.filterInputValue = null;
    this.allCasesSelected = [];
  }
  public filterData() {
    if (!this.cachedData) {
      return;
    }
    let filteredData = [...this.cachedData];

    if (this.caseValue) {
      console.log(filteredData)
      filteredData = filteredData.filter(fd => fd.case.id === this.caseValue.id);

    }

    if (this.filterInputValue) {
      console.log(filteredData)
      filteredData = filteredData.filter(cd => cd.firstName.includes(this.filterInputValue) ||
        cd.lastName.includes(this.filterInputValue));
    }
    this.filteredData = filteredData;
    this.dataSource.data = this.filteredData;
  }
}
