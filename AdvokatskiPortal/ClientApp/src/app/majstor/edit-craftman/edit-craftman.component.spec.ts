import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCraftmanComponent } from './edit-craftman.component';

describe('EditMajstorComponent', () => {
  let component: EditCraftmanComponent;
  let fixture: ComponentFixture<EditCraftmanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCraftmanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCraftmanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
