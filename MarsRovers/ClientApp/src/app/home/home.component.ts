import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

interface Plateau {
  x: number;
  y: number;
}

interface Rover {
  x: number;
  y: number;
  facingTo: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  results = "";
  inputData = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM";
  error = "";
  plateau: Plateau;
  rows: any[];
  columns: any[];
  rovers: Rover[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) { 
  }

  navigate() {
    this.clean();
    this.http.get(this.baseUrl + 'navigation?inputData=' + encodeURI(this.inputData), { responseType: 'text' }).subscribe(result => {
      this.results = result.toString();
      this.drawPlateau();
      this.moveRovers();
    }, error => {
      var errorData = JSON.parse(error.error);
      this.error = errorData.detail;
      console.error(errorData);
    });
  }

  clean() {
    this.error = "";
    this.plateau = null;
  }

  isRoverHere(x, y) {
    return this.rovers.some(rover => rover.x == x && rover.y == y);
  }

  roverFacingTo(x, y) {
    return this.rovers.find(rover => rover.x == x && rover.y == y).facingTo;
  }

  moveRovers() {
    this.rovers = [];
    var roversInfo = this.results.split("\n");
    roversInfo.forEach((value) => {
      var data = value.split(" ");
      var rover: Rover = {x: parseInt(data[0]), y: parseInt(data[1]), facingTo: data[2]};
      this.rovers.push(rover)
    });
  }

  drawPlateau() {
    var parts = this.inputData.split('\n');
    if (parts.length > 0) {
      var coordenates = parts[0].split(' ');
      if (coordenates.length > 1) {
        this.plateau = { x: parseInt(coordenates[0]), y: parseInt(coordenates[1]) };
        this.rows = Array(this.plateau.y+1).fill(0).map((x, i) => i).sort(function (a, b) {
          return b - a;
        });;
        this.columns = Array(this.plateau.x+1).fill(0).map((x, i) => i)
      }
    }
  }

}

