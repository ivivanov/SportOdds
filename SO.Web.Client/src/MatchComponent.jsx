import React, { Component } from 'react';
import Bet from './BetComponent';

class Match extends Component {

    constructor(props) {
        super(props);
        this.ws = props.ws;
        this.state = {
            id: props.data.id,
            name: props.data.name,
            matchType: props.data.matchType,
            startDate: props.data.startDate,
            bets: props.data.bets || []
        };
    }

    render() {
        return (
            <li className='list-group-item'>
                <h4>{this.state.name} / {this.state.startDate} / {this.state.matchType}</h4>
                <ul className='list-group-horizontal'>
                {this.state.bets.map((bet, i)  => (
                        <Bet key={i} data={bet} ws={this.ws}></Bet>
                    ))}
                </ul>
            </li>
        );
    }
}

export default Match;
