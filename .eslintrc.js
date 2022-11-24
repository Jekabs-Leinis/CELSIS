module.exports = {
    root: true,
    env: {
        browser: true,
        es6: true,
        node: true,
    },
    parser: 'vue-eslint-parser',
    extends: ['eslint:recommended', 'prettier'],
    parserOptions: {
        ecmaVersion: 2020,
        sourceType: 'module',
        parser: '@typescript-eslint/parser',
        extraFileExtensions: ['.vue'],
        ecmaFeatures: {
            jsx: true,
        },
    },
    rules: {
        'max-lines': ['error', { max: 500 }],
        semi: 'warn',
        'max-depth': ['warn', { max: 4 }],
        complexity: ['warn', { max: 8 }],
    },
};
