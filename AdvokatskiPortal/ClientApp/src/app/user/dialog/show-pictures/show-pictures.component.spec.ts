import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPicturesComponent } from './show-pictures.component';

describe('ShowPicturesComponent', () => {
  let component: ShowPicturesComponent;
  let fixture: ComponentFixture<ShowPicturesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowPicturesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowPicturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
