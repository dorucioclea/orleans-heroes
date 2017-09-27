import { Injectable } from "@angular/core";
import { Hero } from "./hero.model";
import { AppState } from "../../core/app.state";


@Injectable()
export class HeroSelector {
	getAll() {
		return (state: AppState): Hero[]  => {
			return [];
			// return state.heroes;
		};
	}
}