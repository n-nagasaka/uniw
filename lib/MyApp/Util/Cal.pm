use strict;
use warnings;
use DateTime;

package MyApp::Util::Cal;

# date より前の指定した曜日の日付を求める
sub topday {
    my ($w, $year, $month) = @_;

    my $result = DateTime->new(
        year  => $year,
        month => $month,
        day   => 1,
    );

    my $dow = $result->day_of_week;
    my $dlt = ($dow - $w + 7) % 7;

    if ($dlt == 0) {
        return $result;
    }
    return $result->subtract( days => $dlt );
}

1;
