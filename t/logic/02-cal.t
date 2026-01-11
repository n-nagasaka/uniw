use Test::More;
use MyApp::Util::Cal;
use DateTime;

ok(1, 'true is true');

my $expect = DateTime->new(
    year  => 2026,
    month => 2,
    day   => 1,
);

my $res1 = MyApp::Util::Cal::topday(7, 2026, 2);
is($res1, $expect, '日曜日始まりのカレンダーの 2026/2 月のトップは 2026/2/1');

is(
    MyApp::Util::Cal::topday(1, 2026, 2),
    DateTime->new(year  => 2026, month => 1, day => 26),
    '月曜日始まりのカレンダーの 2026/2 月のトップは 2026/1/26');

is(
    MyApp::Util::Cal::topday(7, 2026, 1),
    DateTime->new(year  => 2025, month => 12, day => 28),
    '日曜日始まりのカレンダーの 2026/1 月のトップは 2025/12/28');

is(
    MyApp::Util::Cal::topday(1, 2026, 1),
    DateTime->new(year  => 2025, month => 12, day => 29),
    '月曜日始まりのカレンダーの 2026/1 月のトップは 2025/12/29');

done_testing;