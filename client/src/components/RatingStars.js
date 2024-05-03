import React from 'react';

const RatingStars = ({ averageRating }) => {

    const fullStars = Math.floor(averageRating);
    const remainder = averageRating - fullStars;
    const stars = [];

    for (let i = 0; i < fullStars; i++) {
        stars.push(<span key={i} className="gold-star">★</span>);
    }

    if (remainder >= 0.5) {
        stars.push(<span key="half-star" className="gold-star">★</span>);
    }

    while (stars.length < 5) {
        stars.push(<span key={stars.length} className="gray-star">★</span>);
    }
    return <div className="rating-stars">{stars}</div>;
};

export default RatingStars;