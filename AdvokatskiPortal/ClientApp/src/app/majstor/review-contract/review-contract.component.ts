//import { caseSlanjeVM } from '../../model/caseSlanjeVM';
import { Contract } from '../../model/Contrct';
import { CraftmanService } from '../../service/Craftman.service'
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTabChangeEvent, MatPaginator, MatSort } from '@angular/material';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ModificationOfferComponent } from '../dialog/modificartion-offer/modificartion-offer.component';
import { FormControl } from '@angular/forms';
import { PreviewCaseCraftmanComponent } from '../dialog/preview-case-craftman/preview-case-craftman.component';

@Component({
  selector: 'app-review-contract',
  templateUrl: './review-contract.component.html',
  styleUrls: ['./review-contract.component.css']
})
export class ReviewContractComponent implements OnInit {
  displayedColumns: string[] = ['firstName', 'lastName', 'name', 'price','startDate','finishDate',
                                'lastDate','dateRecive', 'lastetAnswer' , 'button'];
  public dataSource = new MatTableDataSource<any>();
  nameFilter = new FormControl('');
  tabIndex = new FormControl('');
  contract = new Contract();
  answer: string;
  imageurl;
  allCases: any;
  allCasesSelected;
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
  private _filterInputValue: string;
  public set filterInputValue(val: any) {
    this._filterInputValue = val;
    this.filterData();
  }
  public get filterInputValue() {
    return this._filterInputValue;
  }
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private craftmanService: CraftmanService, public dialog: MatDialog) {
    this.initialize();
  }
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  initialize() {
    this.craftmanService.GetContractFromCraftmans().subscribe(res => {
      this.allCases = res;
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;
      this.handleTabChange(0);
      console.log(this.dataSource.data)

      this.allCases.map(ss => {
        ss.case.pictures.map(s => {
          s.pictureBytes = 'data:image/jpeg;base64,' + s.pictureBytes;
          return s;
        });
        return ss;
      });
    });
  }

  openDialogEdit(element): void {
    console.log(element)
    const dialogRef = this.dialog.open(ModificationOfferComponent, {
      width: '250px',
      data: {
        contract: Object.assign(new Contract(), element),
        submitCallback: this.submitPopupForm.bind(this),
        hideUserOptions: true,
        finishDate: element.finishDate,
        startDate: element.startDate
      }
    });
  }
  submitPopupForm(result) {
    console.log(result)
     this.craftmanService.ModificationCaseFromCraftman(result).subscribe(res => {
     this.initialize() ;
     });
  }
  openDialogReviewCase(element): void {
    const dialogRef = this.dialog.open(PreviewCaseCraftmanComponent, {
      width: '750px',
      height: '900px',
      data: { name: element.case.name, description: element.case.description, pictures: element.case.pictures }
    });
    //ovo mozdane teba proveriti
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      console.log(result)
      this.answer = result;
    });
  }

  removeAt(index: number) {
    const data = this.dataSource.data;
    data.splice(index, 1);
    this.dataSource.data = data;
  }
  redirectToAccept(caseMajstor) {
    const ids = { majstorId: caseMajstor.majstorId, caseId: caseMajstor.case.id }
    this.craftmanService.AcceptCaseFromCraftman(ids).subscribe(res => {
    });
  }

  redirectToReject(caseMajstor) {
    const ids = {majstorId: caseMajstor.majstorId , caseId: caseMajstor.case.id}
      this.craftmanService.RejectCaseFromCraftman(ids).subscribe((res: any) => {
        console.log(res);
      });
  }

  handleCase(ids) {
    this.allCasesSelected = [];
    console.log(this.allCasesSelected)
    ids.forEach(el => {
      const x = [...this.filteredData].find(sso => sso.case.id === el);
      this.allCasesSelected.push(x.case);
    });
  }

  chekData(data):boolean {
    var nowData = new Date().toUTCString();
    var x = data < nowData
    return x ; 
  }

  handleTabChange(tab) {
    switch (tab) {
      case 0:
          const unique0 = [...new Set(this.filteredData.map(item => item.case.id))];
          this.handleCase(unique0);
          console.log(unique0)
        this.dataSource.data = [...this.filteredData];
        break;
      // filter accept
      case 1:
          const unique = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 2).map(item => item.case.id))];
          this.handleCase(unique);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 2);
        break;
      // filter in progress
      case 2:
          const unique1 = [...new Set(this.filteredData.filter(
                        ss => ss.caseStatusId === 6 || ss.caseStatusId === 1 || ss.caseStatusId === 7).map(item => item.case.id))];
           console.log(unique1);
          this.handleCase(unique1);
        this.dataSource.data = [...this.filteredData].filter(ss =>
          ss.caseStatusId === 6 || ss.caseStatusId === 1 || ss.caseStatusId === 7);
        break;
      // filter reject
      case 3:
          const unique2 = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 5).map(item => item.case.id))];
          this.handleCase(unique2);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 5);
        break;
      case 4:
          const unique3 = [...new Set(this.filteredData.filter(ss => ss.caseStatusId === 3).map(item => item.case.id))];
          this.handleCase(unique3);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.caseStatusId === 3);
        break;
      default:
        break;
    }
  }

  handleButton(element) {
    switch (element.caseStatusId) {
      case 4:
        return 'Ceka se odgovor klijenta';
        break;
      case 2:
        return 'Korisnik je prihvatio';
        break;
      case 3:
        return 'Korisnik je odbio ponudu';
        break;
      case 7:
        return 'Ceka se odgovor klijanta';
        break;
      case 5:
        return 'Odbili ste ponudu';
      default:
        break;
    }
  }

  public filterData() {
    if (!this.cachedData) {
      return;
    }
    let filteredData = [...this.cachedData];

    if (this.caseValue) {
      filteredData = filteredData.filter(fd => fd.case.id === this.caseValue.id);
    }

    if (this.filterInputValue) {
      console.log(this.filteredData)
      filteredData = filteredData.filter(cd => cd.firstName.includes(this.filterInputValue) || cd.lastName.includes(this.filterInputValue));
    }
    this.filteredData = filteredData;
    this.dataSource.data = this.filteredData;
  }
}

