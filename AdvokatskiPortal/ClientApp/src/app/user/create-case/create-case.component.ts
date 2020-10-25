import { SuccessfullCreateCaseComponent } from '../../snackBar/successfull-create-case/successfull-create-case.component';
import { Case } from '../../model/Case';
import { Picture } from '../../model/Picture';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../service/User.service';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-create-case',
  templateUrl: './create-case.component.html',
  styleUrls: ['./create-case.component.css']
})
export class CreateCaseComponent implements OnInit {
  case = new Case();
  fileData: File = null;
  pictures : Picture[] = [];
  base64textString = [];
  sirina;
  duzina;
  categories;
  subCategories;
  originalData;
  i = 0;
  reader = new FileReader();
  private fileHandler: ImageHandler;
  constructor(private userService: UserService, private _snackBar: MatSnackBar) { }
  ngOnInit(): void {
    this.userService.GetAllCategories().subscribe((res: any) => {
      this.originalData = res;
      this.categories = [...res].filter(x => !x.parentId);
    });
    this.reader.onload = this.handleReaderLoaded.bind(this);
  }
  openSnackBar() {

  }
  onParentChanged(evt) {
    this.setSubcategories(evt.value);
  }

  setSubcategories(parentId) {
    this.subCategories = [...this.originalData].filter(x => x.parentId === parentId);
  }
  createCase() {
    //this.setGPS(position.coords.latitude, position.coords.longitude);
    this.case.Pictures = this.pictures;
    this.userService.CreateCase(this.case).subscribe( res => {
      this._snackBar.openFromComponent(SuccessfullCreateCaseComponent, {
        duration: 3000
      });
    });
    //TODO ne radi popraviti
    // navigator.geolocation.getCurrentPosition((position) => {
    // });
  }
  setGPS(duzina, sirina) {
    this.case.GDuzina = duzina;
    this.case.GSirina = sirina;
  }
  onUploadChange(evt: any) {
  const file = evt.target.files[0];
    if (file) {
      this.fileHandler = new ImageHandler(file);
      this.reader.readAsDataURL(file);
    }

  }
  displayPicture;
  handleReaderLoaded(e) {
    // console.log(e.target.result)
    const base64 = e.target.result.toString().split(',')[1];
    const prikaz = e.target.result;
    let type ;

    const slika = this.fileHandler.ProcessFile(base64, prikaz, type);
    this.pictures.push(slika);
    this.displayPicture = this.pictures;
    console.log(this.displayPicture)
  }
  deleteImage(img):string {
    this.pictures.forEach(slika => {
      if (slika === img ){
        const index: number = this.pictures.indexOf(img);
        this.pictures.splice(index ,1)
      }
    });
    return img;
  }
}

class ImageHandler {
  private picture: Picture;
  constructor(private file: File) {
    this.picture = new Picture();
  }

  public ProcessFile(base64, prikaz, type): Picture {
    this.handlePictureName();
    this.picture.type = type;
    this.picture.pictureBytes = base64;
    this.picture.show = prikaz;
    return this.picture;
  }
  private handlePictureName() {
    this.picture.Name = this.file.name;
  }
}
