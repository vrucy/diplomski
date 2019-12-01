import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaMajstoraComponent } from './tabela-majstora.component';

describe('TabelaMajstoraComponent', () => {
  let component: TabelaMajstoraComponent;
  let fixture: ComponentFixture<TabelaMajstoraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabelaMajstoraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabelaMajstoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
