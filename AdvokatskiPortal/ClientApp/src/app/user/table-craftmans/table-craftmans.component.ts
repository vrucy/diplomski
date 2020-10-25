import { Craftman } from '../../model/Craftman'
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { UserService } from '../../service/User.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-table-craftmans',
  templateUrl: './table-craftmans.component.html',
  styleUrls: ['./table-craftmans.component.css']
})
export class TableCraftmansComponent implements OnInit {

  displayedColumns: string[] = [ 'FirstName', 'LastName', 'Place', 'Street', 'Email'];
  //advokati;
  categories;
  public dataSource = new MatTableDataSource<Craftman>();
  selection = new SelectionModel<Craftman>(true, []);
  originalData;
  subCategories;
  // selectedId;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  // filter
  private cachedData: any[];
  private filteredData: any[];
  private _categoryValue: any;
  public set kategorijaValue(val: any) {
    this._categoryValue = val;
    this.filterData();
  }
  public get kategorijaValue() {
    return this._categoryValue;
  }
  private _subCategoryValue: any;
  public set subCategoryValue(val: any) {
    this._subCategoryValue = val;
    this.filterData();
  }
  public get subCategoryValue() {
    return this._subCategoryValue;
  }
  private _filterInputValue: string;
  public set filterInputValue(val: any) {
    this._filterInputValue = val;
    this.filterData();
  }
  public get filterInputValue() {
    return this._filterInputValue;
  }
  // end filter
  constructor(private userService: UserService) { }


  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    console.log(this.dataSource)
    this.userService.GetAllCraftmans().subscribe((res: any) => {
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;
    });
      this.userService.GetAllCategories().subscribe((res: any) => {
        console.log(res)
      this.originalData = res;
      this.categories = [...res].filter(x => !x.parentId);
    });
  }
  resetFilters(){
    this.filterInputValue = null;
    this.kategorijaValue = null;
    this.subCategoryValue = null;
  }
  filterData() {
    if (!this.cachedData) {
      return;
    }
    let filteredData = [...this.cachedData];
    // filter by name first
    if (this.filterInputValue) {
      filteredData = filteredData.filter(cd => cd.ime.includes(this.filterInputValue) || cd.prezime.includes(this.filterInputValue));
    }

    if (this.kategorijaValue) {
      filteredData = filteredData.filter(fd => fd.kategorije.find(k => k.kategorija.parentId === this.kategorijaValue.id));
    }
    if (this.subCategoryValue) {
      filteredData = filteredData.filter(fd => fd.kategorije.find(k => k.kategorijaId === this.subCategoryValue.id));
    }

    this.filteredData = filteredData;
    this.dataSource.data = this.filteredData;
  }
  onParentChanged(evt) {
    this.setSubcategories(evt.value);
  }

  setSubcategories(parent) {
    this.subCategories = [...this.originalData].filter(x => x.parentId === parent.id);
  }
}
