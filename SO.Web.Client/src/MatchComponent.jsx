import React, { Component } from 'react';
import Bet from './BetComponent';

class Match extends Component {

    constructor(props) {
        super(props);
        this.ws = props.ws;
        this.state = {
            id: props.data.id,
            name: props.data.name,
            bets: props.data.bets || []
        };
    }

    render() {
        return (
            <li className='list-group-item'>
                <h6>{this.state.name}</h6>
                <ul className='list-group'>
                {this.state.bets.map((bet, i)  => (
                        <Bet key={i} data={bet} ws={this.ws}></Bet>
                    ))}
                </ul>
            </li>
        );
    }
}

export default Match;
