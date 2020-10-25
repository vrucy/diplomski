import { Craftman } from '../../model/Craftman'
import {ChangeDetectorRef } from '@angular/core';
import { SlucajSlanjeVM } from '../../model/SlucajSlanjeVM';
import { UserService } from '../../service/User.service';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';
import { Case } from '../../model/Case';
import { ShowPicturesComponent } from '../dialog/show-pictures/show-pictures.component';
import { MatDialog, MatPaginator, MatSort, MatStepper, MatSnackBar } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { SuccessfullSendCaseComponent } from '../../snackBar/successfull-send-case/successfull-send-case.component';

@Component({
  selector: 'app-send-case',
  templateUrl: './send-case.component.html',
  styleUrls: ['./send-case.component.css']
})
export class SendCaseComponent implements OnInit {
  // cenovnik = { vrstaPlacanja: '', kolicina: '' };
  // vrstaPlacanja;
  displayedColumns: string[] = ['select',  'FirstName', 'LastName', 'Place', 'Street', 'Email'];
  selection = new SelectionModel<Craftman>(true, []);
  public dataSource = new MatTableDataSource<Craftman>();
  displayedColumnsCase: string[] = [ 'Name', 'Descritpion', 'Pictures', 'Actions'];
  public dataSourceCase = new MatTableDataSource<Case>();
  case: Case = new Case();
  NewCase: Case = new Case();
  cases;
  // panelStanje = false;
  SlucajVM: SlucajSlanjeVM = new SlucajSlanjeVM();
  nameFilter = new FormControl('');
  podKategorijaFilter = new FormControl('');
  isLinear = true;
  filterTxt: string;
  SeletedCase;
  // odabraniMajstori;
  allCraftman: any;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  @ViewChild('sort', { static: true }) sort: MatSort;
  // @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild('sort1', { static: true }) sort1: MatSort;
  // @ViewChild(MatPaginator, { static: true }) paginator1: MatPaginator;
  @ViewChild('paginator', { read: MatPaginator, static: true}) paginator: MatPaginator;
  @ViewChild('paginator1', { read: MatPaginator, static: true } ) paginator1: MatPaginator;
  constructor(private _formBuilder: FormBuilder, private userService: UserService,
    private router: Router, public dialog: MatDialog, private cdref: ChangeDetectorRef, private _snackBar: MatSnackBar) {
    // this.dataSource.filterPredicate = this.tableFilter();
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  ngOnInit() {
    this.dataSource.paginator = this.paginator1;
    this.dataSource.sort = this.sort1;
    this.dataSourceCase.sort = this.sort;
    this.dataSourceCase.paginator = this.paginator;
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      items: [ '']
    });
    this.userService.GetAllCraftmans().subscribe((res: any) => {
      this.allCraftman = res;
    });

    this.userService.GetAllCaseForUser().subscribe((res: any) => {
      this.cases = res;
      this.dataSourceCase.data = this.cases;
      this.remapImagesForDisplay(res);
    });
  }
  getMajstoriBySelectedId() {
    const searchTerm = this.SeletedCase.categoryId;
    console.log(this.allCraftman)

    this.dataSource.data = [...this.allCraftman].filter(r => r.categories.some(k => k.categoryId === searchTerm));
  }
  openDialogShowPicture(element): void {
    const dialogRef = this.dialog.open(ShowPicturesComponent, {
      width: '40%',
      height: '80%',
      data: { pictures: element.pictures }
    });

  }
  private remapImagesForDisplay(data) {
    data.forEach(c => {
      const baseSlike = c.pictures.map(s => {
        s.pictureBytes = 'data:image/jpeg;base64,' + s.pictureBytes;
        return s;
      });
    });
  }
  acceptSlucaj(slucaj, stepper: MatStepper) {
    console.log(slucaj)
    slucaj.slike.forEach((slika: any) => {
      if (slika.slikaProp) {
        const base64result = slika.slikaProp.split(',')[1];
        slika.slikaProp = base64result;
      }
    });
    this.SeletedCase = slucaj;
    this.firstFormGroup.get('firstCtrl').setValue('true') ;
    stepper.next();
  }
  resetFilter() {
    this.nameFilter.reset();
  }
  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: Craftman): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }
  resetStepper(stepper) {
    console.log(this.selection)
    stepper.reset();
    this.selection.clear();
    console.log(this.SeletedCase)
     this.dataSource.data = null;
     this.SeletedCase = null;
    console.log(this.SeletedCase)
  }
  onSubmit(){

  }
  validateForm(c, stepper: MatStepper){
    this.firstFormGroup.get('firstCtrl').setValue('true') ;
    c.pictures.forEach((picture: any) => {
      if (picture.pictureBytes) {
        const base64result = picture.pictureBytes.split(',')[1];
        picture.pictureBytes = base64result;
      }
    });
    this.SeletedCase = c;
    stepper.next();
  }
  save() {
    this.SlucajVM.Craftmans = this.selection.selected;
    this.SlucajVM.Case = this.SeletedCase;
    this.userService.PostCaseCraftmans(this.SlucajVM).subscribe(res => {
      this._snackBar.openFromComponent(SuccessfullSendCaseComponent, {
        duration: 3000
      });
    });
  }

  stepChanges(step) {
    if (step.selectedIndex === 1) {
      this.getMajstoriBySelectedId();
    }
  }

}
