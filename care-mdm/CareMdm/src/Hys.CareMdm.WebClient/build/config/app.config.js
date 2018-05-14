let env = (process.env.NODE_ENV || 'development').trim();

module.exports = {
    debug: false,
    isDev: env === 'development',
    isPro: env === 'production',
};